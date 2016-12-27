using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace LogReader
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : ModernWindow, INotifyPropertyChanged
	{
		const string DefaultSteamDirectory = @"C:\program files (x86)\steam\steamapps\common\Crypt of the Necrodancer";
		const string TestDir = @"C:\TFS\LogReader\Tests";
		const string TestFile = "1.txt";

		string ndDir;

		public MainWindow()
		{
			NDDirectory = DefaultSteamDirectory;
			NDDirectory = TestDir;
			//InitializeComponent();
		}

		public string NDDirectory
		{
			get { return ndDir; }
			set
			{
				if (value != ndDir)
				{
					ndDir = value;
					NotifyPropertyChanged();
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		// This method is called by the Set accessor of each property.
		// The CallerMemberName attribute that is applied to the optional propertyName
		// parameter causes the property name of the caller to be substituted as an argument.
		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void btnOpenLog_Click(object sender, RoutedEventArgs e)
		{
			if (CommonFileDialog.IsPlatformSupported)
			{
				CmnFileDialog();
			}
			else
			{
				WinFormsDialog();
			}
		}

		void CmnFileDialog()
		{
			using (var dialog = new CommonOpenFileDialog())
			{
				dialog.IsFolderPicker = true;
				dialog.DefaultDirectory = NDDirectory;
				dialog.Title = "Select Necrodancer Install Folder";
				CommonFileDialogResult result = dialog.ShowDialog();
				if (result == CommonFileDialogResult.Ok)
				{
					NDDirectory = dialog.FileName;
				}
			}
		}

		void WinFormsDialog()
		{
			using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
			{
				dialog.ShowNewFolderButton = false;
				dialog.SelectedPath = NDDirectory;
				var result = dialog.ShowDialog();
				if (result == System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Yes)
				{
					NDDirectory = dialog.SelectedPath;
				}
			}
		}

		void Main()
		{
		}

		private void btnOpenReader_Click(object sender, RoutedEventArgs e)
		{
			LReader reader = new LReader(NDDirectory);
			reader.Main();
		}
	}
}
