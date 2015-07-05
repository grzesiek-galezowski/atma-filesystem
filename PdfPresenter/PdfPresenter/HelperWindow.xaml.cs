using System.Windows;
using System.Windows.Media;

namespace PdfPresenter
{
  /// <summary>
  /// Interaction logic for HelperWindow.xaml
  /// </summary>
  public partial class HelperWindow : Window
  {
    private readonly Slideshow _currentSlide;
    private readonly Slideshow _nextSlide;

    public HelperWindow(Slideshow currentSlide, Slideshow nextSlide)
    {
      _currentSlide = currentSlide;
      _nextSlide = nextSlide;

      InitializeComponent();
      this.Background = new SolidColorBrush(Colors.Black);
    }

    private void HelperWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      _currentSlide.Load();
      _nextSlide.Load();

      HelpCurrentSlide.Children.Add(_currentSlide.ToWindowsFormsHost());
      HelpNextSlide.Children.Add(_nextSlide.ToWindowsFormsHost());
    }

    private void HelperWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      _currentSlide.Refresh();
      _nextSlide.Refresh();
    }
  }
}
