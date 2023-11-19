using QLQuanCafe.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QLQuanCafe
{
	public partial class AppShell : Shell
	{
		public AppShell ()
		{
			InitializeComponent ();

            // Tạo ShellItem và ShellContent
            var shellItem = new ShellItem { Route = "login", Title = "Login" };
            var shellContent = new ShellContent { ContentTemplate = new DataTemplate(typeof(QLQuanCafe.Views.LoginPage)) };

            shellItem.Items.Add(shellContent);
            Items.Add(shellItem);
        }
    }
}