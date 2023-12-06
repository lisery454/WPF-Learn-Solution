using System.Text;

namespace ReadData;

public class MainDataReader
{
    private string _path;
    private FileStream _fileStream;
    private int _frequency;
    private byte[] buffer = new byte[Width];
    private StringBuilder _stringBuilder;
    public const int Width = 20;

    public TimeSpan TotalTime { get; private set; }

    public MainDataReader(string filePath, int frequency)
    {
        _path = filePath;
        _frequency = frequency;
        GetTotalTime();
        _fileStream = new FileStream(filePath, FileMode.Open);
        _stringBuilder = new StringBuilder();
    }

    private void GetTotalTime()
    {
        var fileInfo = new FileInfo(_path);
        TotalTime = TimeSpan.FromSeconds(fileInfo.Length / 20f / _frequency);
    }

    public List<double> GetData(TimeSpan beginTime, TimeSpan lastTime)
    {
        var data = new List<double>();
        var beginCount = (long)(beginTime.TotalSeconds * _frequency);
        var lastCount = (long)(lastTime.TotalSeconds * _frequency);

        _fileStream.Seek(beginCount * Width, SeekOrigin.Begin);
        const int length = 2048;
        var array = new byte[length];
        int bytesRead;
        var str = new StringBuilder();
        char ch;
        int i;
        long currentCount = 0;
        while ((bytesRead = _fileStream.Read(array, 0, length)) > 0)
        {
            for (i = 0; i < bytesRead; i++)
            {
                ch = (char)array[i];
                if (ch == '\r')
                {
                }
                else if (ch == '\n')
                {
                    if (currentCount < lastCount)
                    {
                        if (double.TryParse(str.ToString(), out var num))
                        {
                            data.Add(num);
                            str.Clear();
                            currentCount++;
                        }
                    }
                    else
                    {
                        return data;
                    }
                }
                else
                {
                    str.Append(ch);
                }
            }
        }

        if (currentCount < lastCount)
        {
            if (double.TryParse(str.ToString(), out var num))
            {
                data.Add(num);
            }
        }

        return data;
    }

    public double ValueAt(TimeSpan time)
    {
        _fileStream.Seek((int)(time.TotalSeconds * _frequency) * Width, SeekOrigin.Begin);
        var read = _fileStream.Read(buffer, 0, Width);
        if (read == Width)
        {
            _stringBuilder.Clear();
            foreach (var t in buffer) _stringBuilder.Append((char)t);
            if (double.TryParse(_stringBuilder.ToString(), out var res))
                return res;
        }

        throw new Exception($"read {_stringBuilder} wrong");
    }
}