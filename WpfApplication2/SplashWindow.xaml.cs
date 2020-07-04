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
using System.Windows.Media.Animation;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Threading;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow main = new MainWindow();

            if (main != null)
                main.Show();
            else 
            {
                main = new MainWindow();
                main.Show();
            }//else...
            
            if(this!=null)
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //myImage.Sto
           // var fadeInAnimation = new DoubleAnimation(1d, new TimeSpan(10000));

           // if (myImage.Source != null)
            /*{
                //var fadeOutAnimation = new DoubleAnimation(0d, fadeOutTime);

                fadeOutAnimation.Completed += (o, e) =>
                {
                    image.Source = source;
                    image.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
                };

                image.BeginAnimation(Image.OpacityProperty, fadeOutAnimation);
            }*/
            //else
            {
              //  myImage.Opacity = 0.0f;
                //myImage.Source = source;
                //myImage.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
            }
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            //TypewriteTextblock("CARE Social Media Analyzer", productName, new TimeSpan(15000));

            //TypeText("CARE Social Media Analyzer");

            ButtonAutomationPeer peer =new ButtonAutomationPeer(myButton);
            IInvokeProvider invokeProv =peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();

        }//func...

        public void TypeText(string text)
        {
            foreach (var c in text)
            {
                productName.Text += c.ToString();//.SendKeyStroke(c);
                Thread.Sleep(TimeSpan.FromSeconds(200));
            }
        }//func...

        public void TypewriteTextblock(string textToAnimate, TextBlock txt, TimeSpan timeSpan)
    {
        Storyboard story = new Storyboard();
        story.FillBehavior = FillBehavior.HoldEnd;
        story.RepeatBehavior = RepeatBehavior.Forever;

        DiscreteStringKeyFrame discreteStringKeyFrame;
        StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
        stringAnimationUsingKeyFrames.Duration = new Duration(timeSpan);

        string tmp = string.Empty;
        foreach(char c in textToAnimate)
        {
            discreteStringKeyFrame = new DiscreteStringKeyFrame();
            discreteStringKeyFrame.KeyTime = KeyTime.Paced;
            tmp += c;
            discreteStringKeyFrame.Value = tmp;
            stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
        }
        Storyboard.SetTargetName(stringAnimationUsingKeyFrames, txt.Name);
        Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath(TextBlock.TextProperty));
        story.Children.Add(stringAnimationUsingKeyFrames);

        story.Begin(txt);
    }//func...

    }
}
