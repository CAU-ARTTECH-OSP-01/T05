using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public static class DataReader_CSV
{
    public static List<Dictionary<string, string>> ReadDataByCSV(string _fileName)
    {
        var _list = new List<Dictionary<string, string>>();

        TextAsset asset = Resources.Load(_fileName) as TextAsset;

        string _str = asset.text;

        StringReader _reader = new StringReader(asset.text);

        int _counter = 0;
        string[] _header = new string[0];

        while (true)
        {
            string _data = _reader.ReadLine();
            if (_data == null) break;
            if (_counter == 0) _header = _data.Split(',');
            else
            {
                string[] _values = _data.Split(',');

                var _entry = new Dictionary<string, string>();
                for (int i = 0; i < _header.Length; i++)
                {
                    string _value = _values[i];
                    string _finalValue = _value;

                    _entry[_header[i]] = _finalValue;
                }
                _list.Add(_entry);
            }
            _counter++;
        }

        return _list;
    }
}
