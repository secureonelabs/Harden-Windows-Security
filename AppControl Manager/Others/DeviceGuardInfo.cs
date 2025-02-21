namespace AppControlManager.Others;

internal sealed class DeviceGuardStatus
{
	internal uint? UsermodeCodeIntegrityPolicyEnforcementStatus { get; set; }
	internal uint? CodeIntegrityPolicyEnforcementStatus { get; set; }
}

internal static class DeviceGuardInfo
{

	// Define the WMI query to get the Win32_DeviceGuard class information
	// private const string query = "SELECT UsermodeCodeIntegrityPolicyEnforcementStatus, CodeIntegrityPolicyEnforcementStatus FROM Win32_DeviceGuard";

	// Define the scope (namespace) for the query
	// private const string scope = @"\\.\root\Microsoft\Windows\DeviceGuard";


	/// <summary>
	/// Get the Device Guard status information from the Win32_DeviceGuard WMI class
	/// </summary>
	/// <returns></returns>
	internal static DeviceGuardStatus? GetDeviceGuardStatus()
	{

		/*

		// Create a ManagementScope object for the WMI namespace
		ManagementScope managementScope = new(scope);

		// Create an ObjectQuery to specify the WMI query
		ObjectQuery objectQuery = new(query);

		// Create a ManagementObjectSearcher to execute the query
		using (ManagementObjectSearcher searcher = new(managementScope, objectQuery))
		{
			// Execute the query and retrieve the results
			foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
			{
				// Create an instance of the custom class to hold the result
				DeviceGuardStatus status = new()
				{
					// Retrieve the relevant properties and assign them to the class
					UsermodeCodeIntegrityPolicyEnforcementStatus = obj["UsermodeCodeIntegrityPolicyEnforcementStatus"] as uint?,
					CodeIntegrityPolicyEnforcementStatus = obj["CodeIntegrityPolicyEnforcementStatus"] as uint?
				};

				return status;  // Return the first instance
			}
		}

		*/


		// TODO: Create a Native AOT compatible source generated COM code that won't rely on System.Management or PowerShell

		string UMscript = "(Get-CimInstance -Namespace \\\"root\\Microsoft\\Windows\\DeviceGuard\\\" -Query \\\"SELECT UsermodeCodeIntegrityPolicyEnforcementStatus FROM Win32_DeviceGuard\\\").UsermodeCodeIntegrityPolicyEnforcementStatus";
		string UMoutput = ProcessStarter.RunCommandWithOutput("powershell.exe", $"-NoProfile -Command \"{UMscript}\"");


		string KMscript = "(Get-CimInstance -Namespace \\\"root\\Microsoft\\Windows\\DeviceGuard\\\" -Query \\\"SELECT CodeIntegrityPolicyEnforcementStatus FROM Win32_DeviceGuard\\\").CodeIntegrityPolicyEnforcementStatus";
		string KMoutput = ProcessStarter.RunCommandWithOutput("powershell.exe", $"-NoProfile -Command \"{KMscript}\"");

		return new DeviceGuardStatus()
		{
			UsermodeCodeIntegrityPolicyEnforcementStatus = uint.Parse(UMoutput),
			CodeIntegrityPolicyEnforcementStatus = uint.Parse(KMoutput)
		};
	}
}
