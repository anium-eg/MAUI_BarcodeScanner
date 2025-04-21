using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_BarcodeScanner.Helpers
{
    class KeyboardHelper
    {
        public static double GetKeyboardHeight()
        {
            // You can customize this based on your platform (Android, iOS, etc.)
            // For instance, on Android, you can get the height of the soft keyboard
            // using platform-specific code or use a general approach if it's sufficient.

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                // Handle Android-specific behavior to calculate the keyboard height
                return 200; // This is an example; replace with actual measurement.
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Handle iOS-specific behavior
                return 250; // Replace with actual keyboard height calculation if needed
            }
            return 0; // Default if no platform-specific logic is required
        }
    }
}
