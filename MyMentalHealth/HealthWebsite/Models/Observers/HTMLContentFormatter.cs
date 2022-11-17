using System;
using System.Drawing;
using MyMentalHealth.Models;
//using System.Windows.UI;
//using System.Windows.UI.Xaml.Controls;
//using System.Windows.UI.Xaml.Media;

namespace MyMentalHealth.Models.Observers
{
    public class HTMLContentFormatter : Interface.IObserver<HTMLContentChangedEventArgs>
    {
        

        public HTMLContentFormatter()
        {
            
        }
        public void Update(object sender, HTMLContentChangedEventArgs e)
        {
            Console.WriteLine("Html content changed");
        }
    }
}

