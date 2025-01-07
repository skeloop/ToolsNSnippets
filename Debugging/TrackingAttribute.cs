
namespace Tools
{
    /// <summary>
    /// Attribute wird auf Feldern und Eigenschaften markiert um ihren Wert auszulesen
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class TrackingAttribute : Attribute
    {
        public static List<TrackingAttribute> TrackingAttributes { get; set; } = new();
        public TrackingAttribute(string title, (int left, int top) position)
        {

        }
    }
}
