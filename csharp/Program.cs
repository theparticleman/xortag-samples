using RestSharp;

const string Url = "https://xortag.azurewebsites.net/";
ResponseViewModel world;
Random rand = new();
var restClient = new RestClient(Url);

Register();

while (true)
{
  switch (rand.Next(4))
  {
    case 0:
      MoveUp();
      break;
    case 1:
      MoveRight();
      break;
    case 2:
      MoveDown();
      break;
    case 3:
      MoveLeft();
      break;
  }
  Thread.Sleep(1000);
}

void MoveUp() => world = restClient.GetJson<ResponseViewModel>($"moveup/{world.Id}");
void MoveDown() => world = restClient.GetJson<ResponseViewModel>($"movedown/{world.Id}");
void MoveRight() => world = restClient.GetJson<ResponseViewModel>($"moveright/{world.Id}");
void MoveLeft() => world = restClient.GetJson<ResponseViewModel>($"moveleft/{world.Id}");
void Look() => world = restClient.GetJson<ResponseViewModel>($"look/{world.Id}");

void Register()
{
  world = restClient.GetJson<ResponseViewModel>("register");
  Console.WriteLine("You have successfully registered!");
  Console.WriteLine("Your player name is {0} and your id is {1}", world.Name, world.Id);
  Thread.Sleep(1000);
}

record ResponseViewModel
{
  public int Id { get; set; }
  public string Name { get; set; }
  public bool IsIt { get; set; }
  public int X { get; set; }
  public int Y { get; set; }
  public int MapWidth { get; set; }
  public int MapHeight { get; set; }
  public List<PlayerViewModel> Players { get; set; }
}

record PlayerViewModel
{
  public int X { get; set; }
  public int Y { get; set; }
  public bool IsIt { get; set; }
}