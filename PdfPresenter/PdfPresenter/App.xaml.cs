using System;
using System.Windows;

namespace PdfPresenter
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    [STAThread]
    protected override void OnStartup(StartupEventArgs e)
    {
      try
      {
        const string path = 
          @"C:\Users\astral\Dysk Google\Shared\Raporty i prezentacje\Konferencje\QualityExcites_2015_Galezowski_Grzegorz_modified.pdf";

        var currentSlide = new Slideshow(path, 0);
        var nextSlide = new Slideshow(path, 1);
        var mainSlideshow = new Slideshow(path);

        mainSlideshow.ReportSlideChangesTo(currentSlide);
        mainSlideshow.ReportSlideChangesTo(nextSlide);


        var helper = new HelperWindow(
          currentSlide, 
          nextSlide
        );

        var mainWindow = new MainWindow(mainSlideshow);
        mainWindow.Show();

        helper.Owner = mainWindow;
        helper.Show();

        mainWindow.FocusOnPdf();

      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message + " The application will exit now.");
        throw;
        //Shutdown(-1);
      }

    }

    protected override void OnExit(ExitEventArgs e)
    {
      base.OnExit(e);
    }
  }
}



