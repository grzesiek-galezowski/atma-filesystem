using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Media;

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
      this.Background = new SolidColorBrush(Colors.Black);
      WindowState = WindowState.Maximized;
      WindowStyle = WindowStyle.None;
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
