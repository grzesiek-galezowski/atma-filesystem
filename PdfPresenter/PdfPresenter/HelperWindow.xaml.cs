using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

    }

    public void GoToNextSlide()
    {
      _currentSlide.Advance();
      _nextSlide.Advance();
    }

    private void HelperWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      _currentSlide.Load();
      _nextSlide.Load();

      HelpCurrentSlide.Children.Add(_currentSlide.ToWindowsFormsHost());
      HelpNextSlide.Children.Add(_nextSlide.ToWindowsFormsHost());
    }
  }
}
