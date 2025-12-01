public static class RoomCodeGenerator
{
    private const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

    public static string Generate(int length = 5)
    {
        var rnd = new System.Random();
        char[] buffer = new char[length];
        for (int i = 0; i < length; i++)
            buffer[i] = chars[rnd.Next(chars.Length)];
        return new string(buffer);
    }
}
