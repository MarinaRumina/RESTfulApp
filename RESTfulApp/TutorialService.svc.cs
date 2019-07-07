using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Http;

namespace RESTfulApp
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TutorialService
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        // Add more operations here and mark them with [OperationContract]

        private static List<string> lst = new List<string>
        {
            "Arrays",
            "Queues",
            "Stacks"
        };

        [WebGet(UriTemplate = "/Tutorial")]
        public string GetAllTutorials() => String.Join(",", lst);

        [WebGet(UriTemplate = "/Tutorial/{TutorialId}")]
        public string GetTutorialByID(string TutorialId)
        {
            int pid;
            if (!Int32.TryParse(TutorialId, out pid))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return lst[pid];
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/Tutorial", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public void AddTutorial(string str) => lst.Add(str);

        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, UriTemplate = "/Tutorial/{TutorialId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public void DeleteTutorial(string TutorialId)
        {
            int pid;

            if(!Int32.TryParse(TutorialId, out pid))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            lst.RemoveAt(pid);
        }
    }
}
