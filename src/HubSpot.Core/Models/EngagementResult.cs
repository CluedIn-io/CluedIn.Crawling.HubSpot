using CluedIn.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class EngagementResult
    {
        public EngagementAssociations associations { get; set; }
        public List<object> attachments { get; set; }
        public EngagementEngagement engagement { get; set; }
        public object metadata { get; set; }
        public List<object> scheduledTasks { get; set; }

        public IEnumerable<object> Converttomail()
        {
            var r = new List<object>();
            var result = new Email
            {
                associations = associations,
                attachments = attachments,
                engagement = engagement,
                scheduledTasks = scheduledTasks
            };
            try
            {
                result.metadata = JsonConvert.DeserializeObject<EmailMetadata>(JsonUtility.Serialize(metadata));

                if (result?.metadata != null)
                {
                    if (result.metadata.from?.email != null) r.Add(result.metadata.from);
                    if (result.metadata.to != null)
                        result.metadata.to.Where(n => n.email != null).ForEach(p => r.Add(p));
                    if (result.metadata.bcc != null)
                        result.metadata.bcc.Where(n => n.email != null).ForEach(p => r.Add(p));
                    if (result.metadata.cc != null)
                        result.metadata.cc.Where(n => n.email != null).ForEach(p => r.Add(p));
                }
            }
            catch { }
           
            r.Add(result);
            foreach (var t in r)
                yield return t;
        }

        public IEnumerable<object> Converttospecific()
        {
            if (engagement?.type != null)
            {
                if (engagement.type == "INCOMING_EMAIL")
                    foreach (var g in Converttomail())
                        yield return g;
                else if (engagement.type == "EMAIL")
                    foreach (var e in Converttomail())
                        yield return e;
                else if (engagement.type == "NOTE")
                {
                    var result = new Note
                    {
                        associations = associations,
                        attachments = attachments,
                        engagement = engagement,
                        scheduledTasks = scheduledTasks,
                        metadata = metadata
                    };
                    yield return result;
                }
                else if (engagement.type == "CALL")
                {
                    var result = new Call
                    {
                        associations = associations,
                        attachments = attachments,
                        engagement = engagement,
                        scheduledTasks = scheduledTasks,
                        metadata = metadata
                    };
                    yield return result;
                }
                else if (engagement.type == "TASK")
                {
                    var result = new Task
                    {
                        associations = associations,
                        attachments = attachments,
                        engagement = engagement,
                        scheduledTasks = scheduledTasks,
                        metadata = metadata
                    };
                    var r = new List<long>();
                    try
                    {
                        var jobject = JObject.Parse(metadata.ToString());
                        if (jobject["reminders"] != null)
                        {
                            r = JsonUtility.Deserialize<List<long>>(jobject["reminders"].ToString());
                        }
                    }
                    catch { }               
                    if (!r.Any()) r.Add(0);
                    foreach (var o in r)
                    {
                        var c = result;
                        c.Reminder = o;
                        yield return c;
                    }
                }
                else if (engagement.type == "MEETING")
                {
                    var result = new Meeting
                    {
                        associations = associations,
                        attachments = attachments,
                        engagement = engagement,
                        scheduledTasks = scheduledTasks,
                        metadata = metadata
                    };
                    var r = new List<long>();
                    try
                    {
                        var jobject = JObject.Parse(metadata.ToString());
                        if (jobject["preMeetingProspectReminders"] != null)
                        {
                            r = JsonUtility.Deserialize<List<long>>(jobject["preMeetingProspectReminders"].ToString());
                        }
                    }
                    catch { }
                    if (!r.Any()) r.Add(0);
                    foreach (var o in r)
                    {
                        var c = result;
                        c.Reminder = o;
                        yield return c;
                    }
                }
                else
                {
                    try
                    {
                        metadata = JsonUtility.Deserialize<EngagementMetadata>(JsonUtility.Serialize(metadata));
                    }
                    catch { }
                    yield return this;
                }
            }
        }
    }
}
