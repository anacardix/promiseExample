using Microsoft.ReactNative.Managed;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.System;
using Windows.UI.Core;

namespace promiseExample
{
	[ReactModule("FileLauncher")]
	internal sealed class FileLauncher
	{
		[ReactMethod("launch")]
		public async Task Launch(string fileName, IReactPromise<JSValue> promise)
		{
			StorageFile fileToLaunch = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello, World")))
			{
				using (var fileStream = await fileToLaunch.OpenStreamForWriteAsync())
				{
					await stream.CopyToAsync(fileStream);
				}
			}

			if (fileToLaunch != null)
			{
				await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
					CoreDispatcherPriority.Normal, async () =>
					{
						var success = await Launcher.LaunchFileAsync(fileToLaunch);

						if (!success)
						{
							promise.Reject(new ReactError { Code = "ERR_LAUNCH_FAILED", Message = "Unable to launch application." });
						}
						else
						{
							promise.Resolve(success);
						}
					});
			}
		}
	}
}
