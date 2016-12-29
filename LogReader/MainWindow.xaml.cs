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
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Reflection;

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

		private void btnSecTest_Click(object sender, RoutedEventArgs e)
		{
			DirectorySecurity sec = Directory.GetAccessControl(NDDirectory);
			var acrl = sec.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
			SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
			sec.AddAccessRule(new FileSystemAccessRule(everyone,
				FileSystemRights.Modify | FileSystemRights.Synchronize,
				InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
				PropagationFlags.None, AccessControlType.Allow));
			Directory.SetAccessControl(NDDirectory, sec);
		}

		private void btnReflectionTest_Click(object sender, RoutedEventArgs e)
		{
			Type t = typeof(Item);
			string target = "Player.Inventory";
			string methodName = "Add";
			string[] argTypeStrs = new string[] { "Item" };
			Type[] argTypes = argTypeStrs.Select(s => Type.GetType(s)).ToArray();
			object[] args = argTypes.Select(t => t.Con)
			string[] tprops = target.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
			List<PropertyInfo> pInfos = new List<PropertyInfo>(tprops.Length);
			object lastObj = this;
			Type lastType = lastObj.GetType();
			PropertyInfo lastPI;
			for (int i = 0; i < tprops.Length; ++i)
			{
				lastPI = lastType.GetProperty(tprops[i]);
				if (lastPI == null)
					throw new Exception(string.Format("Couldn't find property {0} in {1}", tprops[i], target));
				pInfos.Add(lastPI);
				lastType = lastPI.PropertyType;
				lastObj = lastPI.GetValue(lastObj);
			}

			//Type.GetType

			MethodInfo mi = lastType.GetMethod(methodName, argTypes);
			mi.Invoke(lastObj, )
		}

		PlayerObj Player { get; set; }

		public class PlayerObj
		{
			public List<Item> Inventory { get; private set; }

			public PlayerObj() { Inventory = new List<Item>();}

		}

		public class Item
		{

		}
	}
}
