namespace Domain.ViewModels
{
    public class BolVM
    {
        public int Id { get; set; }
        public string AdditionalFee { get; set; }
        public string BolCode { get; set; }
        public string CollectInBehalf { get; set; }
        public int DeliveryType { get; set; }
        public int MerchandiseTypeId { get; set; }
        public float Weight { get; set; }
        public int Quantity { get; set; }
        public string Liabilities { get; set; }
        public string Prepaid { get; set; }
        public string PrepaidTemp { get; set; }
        public string ReceiveDate { get; set; }
        public string ReceiveTime { get; set; }
        public string SendAddress { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string SendDate { get; set; }
        public int BolFromId { get; set; }
        public int BolToId { get; set; }
        public bool IsGuarantee { get; set; }
        public int StatusCode { get; set; }
        public string MixedValue { get; set; }
        public string DeclareValue { get; set; }
        public string Total { get; set; }
        public bool IsOnHand { get; set; } 
    }
}