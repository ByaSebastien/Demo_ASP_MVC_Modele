namespace GUI
{
    public static class Mappers
    {
        public static Game ToGUI(this BLL.Game game)
        {
            return new Game
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Age = game.Age,
                IsCoop = game.IsCoop,
                Nb_Player_Max = game.Nb_Player_Max,
                Nb_Player_Min = game.Nb_Player_Min,
            };
        }
        public static BLL.Game ToBLL(this Game game)
        {
            return new BLL.Game
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Age = game.Age,
                IsCoop = game.IsCoop,
                Nb_Player_Max = game.Nb_Player_Max,
                Nb_Player_Min = game.Nb_Player_Min,
            };
        }
        public static Game ToGame(this GameForm game)
        {
            return new Game
            {
                Name = game.Name,
                Description = game.Description,
                Age = game.Age,
                IsCoop = game.IsCoop,
                Nb_Player_Max = game.Nb_Player_Max,
                Nb_Player_Min = game.Nb_Player_Min,
            };
        }
        public static Member ToGUI(this BLL.Member member)
        {
            return new Member
            {
                Id = member.Id,
                Pseudo = member.Pseudo,
                Email = member.Email,
                Password = member.Password
            };
        }
        public static BLL.Member ToBLL(this Member member)
        {
            return new BLL.Member
            {
                Id = member.Id,
                Pseudo = member.Pseudo,
                Email = member.Email,
                Password = member.Password
            };
        }
        public static Member ToMember(this MemberForm member)
        {
            return new Member
            {
                Pseudo = member.Pseudo,
                Email = member.Email,
                Password = member.Password
            };
        }
        public static Member ToMember(this MemberConnection member)
        {
            return new Member
            {
                Pseudo = member.Pseudo,
                Password = member.Password
            };
        }
        public static MemberSession ToMemberSession(this BLL.Member member)
        {
            return new MemberSession
            {
                Id = member.Id,
                Pseudo = member.Pseudo,
                Email = member.Email
            };
        }
        public static MemberDetail ToMemberDetail(this MemberSession member, IEnumerable<BLL.Game> games)
        {
            return new MemberDetail
            {
                Id = member.Id,
                Pseudo = member.Pseudo,
                Email = member.Email,
                Favorites = games.Select(g => g.ToGUI())
            };
        }
    }
}
