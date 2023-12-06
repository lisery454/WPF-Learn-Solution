using System.Text;

namespace ReadData;

public static class DataReformat
{
    public static void ReformatMain(string filePath, string newFilePath)
    {
        const int length = 2048;
        var array = new byte[length];
        int bytesRead;
        var str = new StringBuilder();
        char ch;
        int i;
        int num;
        if (File.Exists(newFilePath))
            File.Delete(newFilePath);
        File.Create(newFilePath).Close();

        using var fileStream = new BufferedStream(new FileStream(filePath, FileMode.Open));
        using var newFileStream = new StreamWriter(newFilePath);
        while ((bytesRead = fileStream.Read(array, 0, length)) > 0)
        {
            for (i = 0; i < bytesRead; i++)
            {
                ch = (char)array[i];
                if (ch != ',')
                {
                    str.Append(ch);
                }
                else
                {
                    if (int.TryParse(str.ToString(), out num))
                    {
                        newFileStream.WriteLine("{0,18:###############}", num);
                    }

                    str.Clear();
                }
            }
        }

        if (int.TryParse(str.ToString(), out num))
        {
            newFileStream.WriteLine("{0,18:###############}", num);
        }
    }

    public static void ReformatRPeaks(string filePath, string newFilePath)
    {
        const int length = 2048;
        var array = new byte[length];
        int bytesRead;
        var str = new StringBuilder();
        char ch;
        int i;
        int num;
        if (File.Exists(newFilePath))
            File.Delete(newFilePath);
        File.Create(newFilePath).Close();

        using var fileStream = new BufferedStream(new FileStream(filePath, FileMode.Open));
        using var newFileStream = new StreamWriter(newFilePath);
        while ((bytesRead = fileStream.Read(array, 0, length)) > 0)
        {
            for (i = 0; i < bytesRead; i++)
            {
                ch = (char)array[i];
                if (ch != ',')
                {
                    str.Append(ch);
                }
                else
                {
                    if (int.TryParse(str.ToString(), out num))
                    {
                        newFileStream.WriteLine("{0,12:##########}", num);
                    }

                    str.Clear();
                }
            }
        }

        if (int.TryParse(str.ToString(), out num))
        {
            newFileStream.WriteLine("{0,12:##########}", num);
        }
    }
}