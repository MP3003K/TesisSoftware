﻿
using Domain.Exceptions.Base;

namespace Application.Exceptions
{
    public class AlgunaEntidadTambienTieneEsteValorException: BaseException
    {
        private const string Entity = "Entity";
        private const string Value = "Value";
        private const string MessageKey = $"Existe otra Entidad {{{Entity}}} Con Este Valor: {{{Value}}}";
        
        public AlgunaEntidadTambienTieneEsteValorException(string entity, string value) : base(MessageKey) {
            AddOrReplaceValue(Entity, entity);
            AddOrReplaceValue(Value, value);
        }
    }
}
