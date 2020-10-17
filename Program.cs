using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Threading.Tasks;

namespace demogit
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri orUrl = new Uri("https://dev.azure.com/flgsolutions10971");
            String personalAccesToken = "i3r7b7bmv77yv52duqvuaur4mskq2psf4vcrrblkorflkvfhwdxq";
            int workItemId = 2;


            VssConnection connection = new VssConnection(orUrl, new VssBasicCredential(string.Empty, personalAccesToken));

            ShowWorkItemDetails(connection, workItemId).Wait();
        }

        static private async Task ShowWorkItemDetails(VssConnection connection, int workItemId)
        {
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            try
            {
                WorkItem workitem = await witClient.GetWorkItemAsync(workItemId);

                foreach (var field in workitem.Fields)
                {
                    Console.WriteLine("{0}: {1}", field.Key, field.Value);
                }
            }
            catch (AggregateException aex)
            {
                VssServiceException vssex = aex.InnerException as VssServiceException;
                if (vssex != null)
                {
                    Console.WriteLine(vssex.Message);
                }
            }
        }
    }
}