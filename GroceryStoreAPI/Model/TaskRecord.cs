namespace GroceryStoreAPI.Model
{
    public class TaskRecord
    {
        public string? task_name { get; set; }
        public Tags tags { get; set; }
        public DateTime due_date { get; set; }
        public string? color { get; set; }
        public string? assigned_to { get; set; }
        public string? status { get; set; }
        public Activity activity { get; set; }
    }

    public class Tags
    {
        public string tagName {  get; set; }
    }
}
