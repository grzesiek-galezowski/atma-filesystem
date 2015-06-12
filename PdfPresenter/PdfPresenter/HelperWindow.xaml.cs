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
    private PresentationRenderer _currentSlide;
    private PresentationRenderer _nextSlide;

    public HelperWindow()
    {
      InitializeComponent();

    }

    public void GoToNextSlide()
    {
      _currentSlide.Advance();
      _nextSlide.Advance();
    }

    private void HelperWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      _currentSlide = new PresentationRenderer(0);
      HelpCurrentSlide.Children.Add(_currentSlide.ToWindowsFormsHost());
      _nextSlide = new PresentationRenderer(1);
      HelpNextSlide.Children.Add(_nextSlide.ToWindowsFormsHost());

      _nextSlide.Advance();
    }
  }
}
