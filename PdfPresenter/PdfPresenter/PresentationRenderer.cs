using System;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Forms.Integration;
using PdfiumViewer;

internal class PresentationRenderer
{
  private readonly PdfRenderer _pdfRenderer;
  private readonly WindowsFormsHost _windowsFormsHost;

  public PresentationRenderer(int startingSlide = 0)
  {
    _pdfRenderer = new PdfRenderer();
    _pdfRenderer.Load(PdfDocument.Load(
      @"C:\Users\astral\Dysk Google\Shared\Raporty i prezentacje\Konferencje\QualityExcites_2015_Galezowski_Grzegorz_modified.pdf"));

    _windowsFormsHost = new WindowsFormsHost
    {
      Child = _pdfRenderer
    };

    _pdfRenderer.Page = startingSlide;
  }

  public void OnKeyUpGoToNextSlide()
  {
    _pdfRenderer.KeyUp += (o, args) => { _pdfRenderer.Page++; };
  }

  public WindowsFormsHost ToWindowsFormsHost()
  {
    return _windowsFormsHost;
  }

  public void AddNextSlideHandler(Action goToNextSlide)
  {
    _pdfRenderer.KeyUp += (sender, args) => goToNextSlide(); 
  }

  public void Advance()
  {
    _pdfRenderer.Page++;
    _pdfRenderer.Refresh();
  }
}