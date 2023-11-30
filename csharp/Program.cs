using RestSharp;

const string Url = "https://xortag.azurewebsites.net/";
ResponseViewModel world;
Random rand = new();
var restClient = new RestClient(Url);

Register();
Move();

void Move()
{
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
  }
}

void MoveUp() => world = SendMoveCommand("moveUp");
void MoveDown() => world = SendMoveCommand("moveDown");
void MoveRight() => world = SendMoveCommand("moveRight");
void MoveLeft() => world = SendMoveCommand("moveleft");

ResponseViewModel SendMoveCommand(string moveCommand)
{
  Thread.Sleep(1000); //Requests more frequent than once per second will fail.
  CheckForRegistration();
  return restClient.GetJson<ResponseViewModel>($"{moveCommand}/{world.Id}");
}

void Look()
{
  Thread.Sleep(1000); //Requests more frequent than once per second will fail.
  CheckForRegistration();
  world = restClient.GetJson<ResponseViewModel>($"look/{world.Id}");
}

void CheckForRegistration()
{
  if (world == null) throw new InvalidOperationException("You have to register as a player before you can move");
}

void Register()
{
  world = restClient.GetJson<ResponseViewModel>("register");
  Console.WriteLine("You have successfully registered!");
  Console.WriteLine("Your player name is {0} and your id is {1}", world.Name, world.Id);
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