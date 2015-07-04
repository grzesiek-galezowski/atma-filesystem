using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PdfiumViewer;
using Path = System.IO.Path;

namespace PdfPresenter
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private WindowsFormsHost _pdfControl;
    private readonly Slideshow _slideshow;

    public MainWindow(Slideshow mainSlideshow)
    {
      _slideshow = mainSlideshow;
      InitializeComponent();
    }


    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      // Create the interop host control.
      _slideshow.Load();
      _slideshow.OnKeyUpGoToNextSlide();
      _pdfControl = _slideshow.ToWindowsFormsHost();

      MainGrid.Children.Add(_pdfControl);

    }

    public void FocusOnPdf()
    {
      this.Focus();
      _pdfControl.Focus();
    }
  }
}
