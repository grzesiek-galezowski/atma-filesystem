using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ImageMagick;

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

        //MessageBox.Show("Initializing all images, preparing presentation, please wait...");

        MagickReadSettings settings = new MagickReadSettings();
        // Settings the density to 300 dpi will create an image with a better quality
        settings.Density = new PointD(100, 100);

        using (MagickImageCollection images = new MagickImageCollection())
        {
          // Add all the pages of the pdf file to the collection
          //images.Read(@"C:\Users\astral\Dysk Google\Shared\Raporty i prezentacje\Konferencje\QualityExcites_2015_Galezowski_Grzegorz_modified.pdf", settings);

          //MessageBox.Show("Images read, now converting to image set, please wait...");

          HelperWindow helper = new HelperWindow();
          var mainWindow = new MainWindow(helper);
          mainWindow.Show();

          helper.Owner = mainWindow;
          helper.Show();
        }

        
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message + " The application will exit now.");
        Shutdown(-1);
      }

    }

    protected override void OnExit(ExitEventArgs e)
    {
      base.OnExit(e);
    }
  }
}



