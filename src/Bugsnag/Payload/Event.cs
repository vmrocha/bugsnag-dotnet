using System.Collections.Generic;
using System.Linq;

namespace Bugsnag.Payload
{
  /// <summary>
  /// Represents a single event in a Bugsnag payload.
  /// </summary>
  public class Event : Dictionary<string, object>
  {
    public Event(string payloadVersion, App app, Device device, System.Exception exception, Severity severity, IEnumerable<Breadcrumb> breadcrumbs, Session session)
    {
      this.AddToPayload("payloadVersion", payloadVersion);
      this.AddToPayload("exceptions", new Exceptions(exception, 5).ToArray());
      this.AddToPayload("app", app);
      this.AddToPayload("device", device);
      this.AddToPayload("metaData", new Metadata());
      this.AddToPayload("breadcrumbs", breadcrumbs);
      this.AddToPayload("session", session);

      foreach (var item in severity)
      {
        this[item.Key] = item.Value;
      }
    }

    public Exception[] Exceptions
    {
      get { return this["exceptions"] as Exception[]; }
      set { this.AddToPayload("exceptions", value); }
    }

    public string Context
    {
      get { return this["context"] as string; }
      set { this.AddToPayload("context", value); }
    }

    public string GroupingHash
    {
      get { return this["groupingHash"] as string; }
      set { this.AddToPayload("groupingHash", value); }
    }

    public Metadata Metadata
    {
      get { return this["metaData"] as Metadata; }
    }

    public User User
    {
      get { return this["user"] as User; }
      set { this.AddToPayload("user", value); }
    }
  }
}
