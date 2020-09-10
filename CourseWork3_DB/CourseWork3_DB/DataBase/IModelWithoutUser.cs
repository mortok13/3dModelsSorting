namespace CourseWork3_DB
{
    public interface IModelWithoutUser
    {
        int Id { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        string Category { get; set; }
        string Status { get; set; }
        string Path { get; set; }
        string GroupName { get; }
    }
}