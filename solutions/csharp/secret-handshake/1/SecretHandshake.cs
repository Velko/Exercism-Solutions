using System;
using System.Collections.Generic;

public static class SecretHandshake
{
    public static string[] Commands(int commandValue)
    {
        var commands = new List<string>();

        if ((commandValue & 0b00001) != 0)
            commands.Add("wink");

        if ((commandValue & 0b00010) != 0)
            commands.Add("double blink");

        if ((commandValue & 0b00100) != 0)
            commands.Add("close your eyes");

        if ((commandValue & 0b01000) != 0)
            commands.Add("jump");

        if ((commandValue & 0b10000) != 0)
            commands.Reverse();

        return commands.ToArray();
    }
}
