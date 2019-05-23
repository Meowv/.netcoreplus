using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plus.Reflection
{
    /// <summary>
    /// TypeFinder
    /// </summary>
    public class TypeFinder : ITypeFinder
    {
        public ILogger Logger { get; set; }

        private readonly IAssemblyFinder _assemblyFinder;
        private readonly object _syncObj = new object();
        private Type[] _types;

        public TypeFinder(IAssemblyFinder assemblyFinder)
        {
            _assemblyFinder = assemblyFinder;
            Logger = NullLogger.Instance;
        }

        public Type[] Find(Func<Type, bool> predicate)
        {
            return GetAllTypes().Where(predicate).ToArray();
        }

        public Type[] FindAll()
        {
            return GetAllTypes().ToArray();
        }

        private Type[] GetAllTypes()
        {
            if (_types == null)
            {
                lock (_syncObj)
                {
                    if (_types == null)
                    {
                        _types = CreateTypeList().ToArray();
                    }
                }
            }

            return _types;
        }

        private List<Type> CreateTypeList()
        {
            var allTypes = new List<Type>();

            var assemblies = _assemblyFinder.GetAllAssemblies().Distinct();

            foreach (var assembly in assemblies)
            {
                try
                {
                    Type[] typesInThisAssembly;

                    try
                    {
                        typesInThisAssembly = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        typesInThisAssembly = ex.Types;
                    }

                    if (typesInThisAssembly.IsNullOrEmpty())
                    {
                        continue;
                    }

                    allTypes.AddRange(typesInThisAssembly.Where(type => type != null));
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.ToString(), ex);
                }
            }

            return allTypes;
        }

        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(T), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(assignTypeFrom, _assemblyFinder.GetAllAssemblies(), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            List<Type> list = new List<Type>();
            try
            {
                foreach (Assembly assembly in assemblies)
                {
                    Type[] array = null;
                    try
                    {
                        array = assembly.GetTypes();
                    }
                    catch
                    {
                    }
                    if (array != null)
                    {
                        Type[] array2 = array;
                        foreach (Type type in array2)
                        {
                            if ((assignTypeFrom.IsAssignableFrom(type) || (assignTypeFrom.IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(type, assignTypeFrom))) && !type.IsInterface)
                            {
                                if (onlyConcreteClasses)
                                {
                                    if (type.IsClass && !type.IsAbstract)
                                    {
                                        list.Add(type);
                                    }
                                }
                                else
                                {
                                    list.Add(type);
                                }
                            }
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                string text = string.Empty;
                Exception[] loaderExceptions = ex.LoaderExceptions;
                foreach (Exception ex2 in loaderExceptions)
                {
                    text = text + ex2.Message + Environment.NewLine;
                }
                Exception ex3 = new Exception(text, ex);
                throw ex3;
            }
            return list;
        }

        protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                Type genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
                Type[] array = type.FindInterfaces((Type objType, object objCriteria) => true, null);
                foreach (Type type2 in array)
                {
                    if (type2.IsGenericType)
                    {
                        return genericTypeDefinition.IsAssignableFrom(type2.GetGenericTypeDefinition());
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}