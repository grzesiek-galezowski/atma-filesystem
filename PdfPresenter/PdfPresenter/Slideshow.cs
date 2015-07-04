using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using PdfiumViewer;

namespace PdfPresenter
{
  public class Slideshow
  {
    private readonly int _startingSlide;
    private readonly PdfRenderer _pdfRenderer;
    private WindowsFormsHost _windowsFormsHost;
    private readonly string _path;
    private readonly List<Slideshow> _observers = new List<Slideshow>();

    public Slideshow(string path, int startingSlide = 0)
    {
      _path = path;
      _startingSlide = startingSlide;
      _pdfRenderer = new PdfRenderer();
    }

    public void Load()
    {
      _pdfRenderer.Load(PdfDocument.Load(_path));

      _windowsFormsHost = new WindowsFormsHost
      {
        Child = _pdfRenderer
      };

      AsSoonAsWinformsHostLoadsShowSlide(_startingSlide, _pdfRenderer);
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

    public void Advance()
    {
      _pdfRenderer.Page++;
      _pdfRenderer.Refresh();
      foreach (var slideshow in _observers)
      {
        slideshow.Advance();
      }
    }

    private void GoBack()
    {
      _pdfRenderer.Page--;
      _pdfRenderer.Refresh();
      foreach (var slideshow in _observers)
      {
        slideshow.GoBack();
      }
    }

    public void ReportSlideChangesTo(Slideshow slideshow)
    {
      _observers.Add(slideshow);
    }
  }
}