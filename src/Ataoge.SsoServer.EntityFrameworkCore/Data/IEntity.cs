using System;

namespace Ataoge.Data
{
    public interface IEntity
    {
        
    }

    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id {get; set;}
    }


   
    public interface ITreeEntity<TKey> : IEntity<TKey>
       where TKey : struct, IEquatable<TKey>
    {
        TKey? Pid {get; set;}
    }

    public interface ITreeEntity : IEntity<string>
    {
        string Pid {get; set;}
    }
    
}