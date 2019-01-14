using DomainFacade.DataLayer.DbManifest;
using System;

namespace DomainFacade.DataLayer.Models
{
    public class DbParamsModel : IDbParamsModel
    {
        public bool Validate<E>(E dbMethod) where E : DbMethodsCore { return true; }
    }
    public abstract class SimpleDbParamsModelBase <T> : IDbParamsModel
    {
        public bool Validate<E>(E dbMethod) where E : DbMethodsCore {
            return ValidateCore(dbMethod);
        }
        protected abstract bool ValidateCore<E>(E dbMethod) where E : DbMethodsCore;
    }
    public class SimpleDbParamsModel<T> : SimpleDbParamsModelBase<T>
    {
        public T Param1 { get; private set; }
        public SimpleDbParamsModel(T param1) : base() { Param1 = param1; }
        protected Func<SimpleDbParamsModel<T>, DbMethodsCore, bool>  Validator  { get; set; } 
        public SimpleDbParamsModel<T> AddValidator(Func<SimpleDbParamsModel<T>, DbMethodsCore, bool> validator)
        {
            Validator = validator;
            return this;
        }
        protected override bool ValidateCore<E>(E dbMethod) {
            if(Validator != null)
            {
                return Validator(this, dbMethod);
            }
            return true;
        }


    }
    public class SimpleDbParamsModel<T, U> : SimpleDbParamsModel<T>
    {
        public U Param2 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2) : base(param1) { Param2 = param2; }
        protected new Func<SimpleDbParamsModel<T, U>, DbMethodsCore, bool> Validator { get; private set; }
        public SimpleDbParamsModel<T, U> AddValidator(Func<SimpleDbParamsModel<T, U>, DbMethodsCore, bool> validator)
        {
            Validator = validator;
            return this;
        }
        protected override bool ValidateCore<E>(E dbMethod)
        {
            if (Validator != null)
            {
                return Validator(this, dbMethod);
            }
            return true;
        }

    }
    public class SimpleDbParamsModel<T, U, V> : SimpleDbParamsModel<T, U>
    {
        public V Param3 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2, V param3) : base(param1, param2) { Param3 = param3; }
        protected new Func<SimpleDbParamsModel<T, U, V>, DbMethodsCore, bool> Validator { get; private set; }
        public SimpleDbParamsModel<T, U, V> AddValidator(Func<SimpleDbParamsModel<T, U, V>, DbMethodsCore, bool> validator)
        {
            Validator = validator;
            return this;
        }
        protected override bool ValidateCore<E>(E dbMethod)
        {
            if (Validator != null)
            {
                return Validator(this, dbMethod);
            }
            return true;
        }

    }
    public class SimpleDbParamsModel<T, U, V, W> : SimpleDbParamsModel<T, U, V>
    {
        public W Param4 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2, V param3, W param4) : base(param1, param2, param3) { Param4 = param4; }
        protected new Func<SimpleDbParamsModel<T, U, V, W>, DbMethodsCore, bool> Validator { get; private set; }
        public SimpleDbParamsModel<T, U, V, W> AddValidator(Func<SimpleDbParamsModel<T, U, V, W>, DbMethodsCore, bool> validator)
        {
            Validator = validator;
            return this;
        }
        protected override bool ValidateCore<E>(E dbMethod)
        {
            if (Validator != null)
            {
                return Validator(this, dbMethod);
            }
            return true;
        }

    }
    public class SimpleDbParamsModel<T, U, V, W, X> : SimpleDbParamsModel<T, U, V, W>
    {
        public X Param5 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2, V param3, W param4, X param5) : base(param1, param2, param3, param4) { Param5 = param5; }
        protected new Func<SimpleDbParamsModel<T, U, V, W, X>, DbMethodsCore, bool> Validator { get; private set; }
        public SimpleDbParamsModel<T, U, V, W, X> AddValidator(Func<SimpleDbParamsModel<T, U, V, W, X>, DbMethodsCore, bool> validator)
        {
            Validator = validator;
            return this;
        }
        protected override bool ValidateCore<E>(E dbMethod)
        {
            if (Validator != null)
            {
                return Validator(this, dbMethod);
            }
            return true;
        }

    }
}
