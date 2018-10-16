namespace Lob.Net.Models
{
    public class UsDeliverabilityAnalysis
    {
        public DpvConfirmation? DpvConfirmation { get; set; }
        public BooleanState? DpvCmra { get; set; }
        public BooleanState? DpvVacant { get; set; }
        public DpvFootNote[] DpvFootnotes { get; set; }
        public bool EwsMatch { get; set; }
        public BooleanState? LacsIndicator { get; set; }
        public string LacsReturnCode { get; set; }
        public string SuiteReturnCode { get; set; }
    }
}
