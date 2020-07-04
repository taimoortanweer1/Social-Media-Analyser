using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Facebook;
using System.Windows.Controls.Primitives;
//using Newtonsoft.Json;
using System.IO;
using mshtml;
using System.Collections.Specialized;
using System.Xml;


using Microsoft.CSharp;
using System.Reflection;
using System.Net;
using System.Collections;
using System.Threading;
using System.Web;
using System.ComponentModel;

namespace WpfApplication2
{
    class WebBrowserHelper
    {
        public static readonly DependencyProperty BodyProperty =
            DependencyProperty.RegisterAttached("Body", typeof(string), typeof(WebBrowserHelper), new PropertyMetadata(OnBodyChanged));

        public static string GetBody(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(BodyProperty);
        }

        public static void SetBody(DependencyObject dependencyObject, string body)
        {
            dependencyObject.SetValue(BodyProperty, body);
        }

        private static void OnBodyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var webBrowser = (WebBrowser)d;
            if (e.NewValue != null && ((string)e.NewValue).Length>0)
            webBrowser.Navigate((string)e.NewValue);
        }
    }//class...
}//namespace...
