using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using PdfiumViewer;

namespace PdfPresenter
{
  public class Slideshow
  {
    private readonly int _slideOffset;
    private readonly PdfRenderer _pdfRenderer;
    private WindowsFormsHost _windowsFormsHost;
    private readonly string _path;
    private readonly List<Slideshow> _observers = new List<Slideshow>();
    private int _currentPage;

    public Slideshow(string path, int slideOffset = 0)
    {
      _path = path;
      _slideOffset = slideOffset;
      _currentPage = slideOffset;
      _pdfRenderer = new PdfRenderer();
    }

    public void Load()
    {
      _pdfRenderer.Load(PdfDocument.Load(_path));

      _windowsFormsHost = new WindowsFormsHost
      {
        Child = _pdfRenderer
      };

      AsSoonAsWinformsHostLoadsShowSlide(_slideOffset, _pdfRenderer);
    }

    private void AsSoonAsWinformsHostLoadsShowSlide(int startingSlide, PdfRenderer pdfRenderer)
    {
      _windowsFormsHost.Loaded += (sender, args) => pdfRenderer.Page = startingSlide;
    }

    public void OnKeyUpGoToNextSlide()
    {
      _pdfRenderer.KeyUp += (o, args) =>
      {
        if (args.KeyCode == Keys.Down)
        {
          Advance();
        }
        else if (args.KeyCode == Keys.Up)
        {
          GoBack();
        }
      };
    }

    public WindowsFormsHost ToWindowsFormsHost()
    {
      return _windowsFormsHost;
    }

    private void Advance()
    {
      _pdfRenderer.Page++;
      _currentPage = _pdfRenderer.Page;
      _pdfRenderer.Refresh();
      NotifyAllObserversOnSlide(_pdfRenderer.Page);
    }

    private void NotifyAllObserversOnSlide(int page)
    {
      foreach (var slideshow in _observers)
      {
        slideshow.NotifySlideChangedTo(page);
      }
    }

    private void NotifySlideChangedTo(int page)
    {
      _pdfRenderer.Page = page + _slideOffset;
      _currentPage = _pdfRenderer.Page;
    }

    private void GoBack()
    {
      _pdfRenderer.Page--;
      _currentPage = _pdfRenderer.Page;
      _pdfRenderer.Refresh();
      NotifyAllObserversOnSlide(_pdfRenderer.Page);
    }

    public void ReportSlideChangesTo(Slideshow slideshow)
    {
      _observers.Add(slideshow);
    }

    public void Refresh()
    {
      _pdfRenderer.Page = _currentPage;
      _pdfRenderer.Refresh();
    }
  }
}