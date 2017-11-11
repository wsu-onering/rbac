using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneRing.Utils
{
	public class PortletDataSource
	{
		static private Dictionary<string, List<string>> _portletAquifers = new Dictionary<string, List<string>>()
		{
			{ "TodoPortlet", new List<string>() { { "http://todostaticsource20171107050947.azurewebsites.net/api/ToDoItems" } } },
		};


		/// <summary>
		/// Retrieves the list of remote data sources registered to a particular portlet.
		/// </summary>
		/// <param name="portletId"></param>
		/// <returns>A list of strings, each string a URL to an endpoint which the portlet may communicate with.</returns>
		public List<string> Registrations(string portletId) {
			if (_portletAquifers.TryGetValue(portletId, out List<string> registrations)) {
				return registrations;
			}
			return null;
		}

		/// <summary>
		/// Registers a given datasource to the specified portlet. If no portletId exists within the datastore, that portletId is added to the datastore with the provided datasource as the only remote data source for the provided portlet.
		/// </summary>
		/// <param name="portletId">The id of the portlet to store the remote data source url under.</param>
		/// <param name="datasource">A url to an endpoint providing data which the portlet understands.</param>
		public void RegisterDataSource(string portletId, string datasource) {
			List<string> registrations;
			if (_portletAquifers.TryGetValue(portletId, out registrations)) {
				registrations.Add(datasource);
			} else {
				registrations = new List<string>() { { datasource } };
				_portletAquifers.Add(portletId, registrations);
			}
		}

	}
}