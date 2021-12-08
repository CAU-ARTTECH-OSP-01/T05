using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public static class DataReader_TSV
{
    public static List<Dictionary<string, string>> ReadDataByTSV(string _fileName)
    {
        var _list = new List<Dictionary<string, string>>();
        StreamReader _reader = new StreamReader(Application.dataPath + "/" + _fileName + ".tsv");

        int _counter = 0;
        string[] _header = new string[0];

        while (true)
        {
            string _data = _reader.ReadLine();
            if (_data == null) break;
            if (_counter == 0) _header = _data.Split('\t');
            else
            {
                string[] _values = _data.Split('\t');

                var _entry = new Dictionary<string, string>();
                for ( int i = 0 ; i < _header.Length; i++)
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
