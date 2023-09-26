using ServerCore;

class PacketHandler
{
	public static void C_LeaveGameHandler(PacketSession session, IPacket packet)
	{
		//C_UserInfoReq p = packet as C_UserInfoReq;

		//Console.WriteLine($"PlayerInfoReq: {p.userId} {p.name}");

		//foreach (C_UserInfoReq.Skill skill in p.skills)
		//{
		//	Console.WriteLine($"Skill({skill.id})({skill.level})({skill.duration})");
		//}
	}

    public static void C_MoveHandler(PacketSession session, IPacket packet)
    {
        //C_UserInfoReq p = packet as C_UserInfoReq;

        //Console.WriteLine($"PlayerInfoReq: {p.userId} {p.name}");

        //foreach (C_UserInfoReq.Skill skill in p.skills)
        //{
        //	Console.WriteLine($"Skill({skill.id})({skill.level})({skill.duration})");
        //}
    }
}
