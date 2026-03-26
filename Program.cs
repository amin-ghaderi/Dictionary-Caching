Catalogue catalogue = new Catalogue();

catalogue.AddToPosts("Friday", 27);
catalogue.AddToPosts("Saturday", 35);
catalogue.AddToPosts("Sunday", 15);
catalogue.AddToPosts("Monday", 50);

var allPosts = catalogue.GetPosts();

foreach (var item in allPosts)
{
    Console.WriteLine($"{item.Key} - {item.Value}");
}

int max = 0;
string bestDay = "";

foreach (var item in allPosts)
{
    if (item.Value > max)
    {
        max = item.Value;
        bestDay = item.Key;
    }
}

Console.WriteLine($"Best day: {bestDay} with value: {max}");

public class Catalogue
{
    private Dictionary<string, int> posts { get; set; } = new Dictionary<string, int>();

    public bool AddToPosts(string text, int number)
    {
        if (posts.ContainsKey(text))
        {
            return false;
        }

        posts[text] = number;
        return true;
    }

    public Dictionary<string, int> GetPosts()
    {
        return posts;
    }
}