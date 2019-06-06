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
                associations = this.associations,
                attachments = this.attachments,
                engagement = this.engagement,
                scheduledTasks = this.scheduledTasks
            };
            try
            {
                result.metadata = JsonConvert.DeserializeObject<EmailMetadata>(JsonUtility.Serialize(this.metadata));

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
            if (this.engagement?.type != null)
            {
                if (this.engagement.type == "INCOMING_EMAIL")
                    foreach (var g in this.Converttomail())
                        yield return g;
                else if (this.engagement.type == "EMAIL")
                    foreach (var e in this.Converttomail())
                        yield return e;
                else if (this.engagement.type == "NOTE")
                {
                    var result = new Note
                    {
                        associations = this.associations,
                        attachments = this.attachments,
                        engagement = this.engagement,
                        scheduledTasks = this.scheduledTasks,
                        metadata = this.metadata
                    };
                    yield return result;
                }
                else if (this.engagement.type == "CALL")
                {
                    var result = new Call
                    {
                        associations = this.associations,
                        attachments = this.attachments,
                        engagement = this.engagement,
                        scheduledTasks = this.scheduledTasks,
                        metadata = this.metadata
                    };
                    yield return result;
                }
                else if (this.engagement.type == "TASK")
                {
                    var result = new Task
                    {
                        associations = this.associations,
                        attachments = this.attachments,
                        engagement = this.engagement,
                        scheduledTasks = this.scheduledTasks,
                        metadata = this.metadata
                    };
                    var r = new List<long>();
                    try
                    {
                        var jobject = JObject.Parse(this.metadata.ToString());
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
                else if (this.engagement.type == "MEETING")
                {
                    var result = new Meeting
                    {
                        associations = this.associations,
                        attachments = this.attachments,
                        engagement = this.engagement,
                        scheduledTasks = this.scheduledTasks,
                        metadata = this.metadata
                    };
                    var r = new List<long>();
                    try
                    {
                        var jobject = JObject.Parse(this.metadata.ToString());
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
                        this.metadata = JsonUtility.Deserialize<EngagementMetadata>(JsonUtility.Serialize(this.metadata));
                    }
                    catch { }
                    yield return this;
                }
            }
        }
    }

    public class Task : EngagementResult
    {
        public long? Reminder { get; set; }
    }

    public class Meeting : EngagementResult
    {
        public long? Reminder { get; set; }
    }

    public class Note : EngagementResult { }
    public class Call : EngagementResult { }
}
