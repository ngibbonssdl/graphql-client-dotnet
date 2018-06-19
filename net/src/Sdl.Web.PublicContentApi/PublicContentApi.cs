﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sdl.Web.PublicContentApi.ContentModel;
using Sdl.Web.GraphQL;
using Sdl.Web.GraphQL.Request;
using Sdl.Web.GraphQL.Response;
using Sdl.Web.GraphQL.Schema;
using System.Threading;
using Sdl.Web.PublicContentApi.ModelServicePlugin;

namespace Sdl.Web.PublicContentApi
{
    /// <summary>
    /// Public Content Api
    /// </summary>
    public class PublicContentApi : IGraphQLClient, IPublicContentApi, IModelServicePluginApi
    {
        private readonly IGraphQLClient _client;

        public PublicContentApi(IGraphQLClient graphQLclient)
        {
            _client = graphQLclient;
        }

        #region IGraphQLClient

        public GraphQLSchema Schema => _client.Schema;

        public Task<GraphQLSchema> SchemaAsync() => _client.SchemaAsync();

        public int Timeout
        {
            get { return _client.Timeout; }
            set { _client.Timeout = value; }
        }

        public IGraphQLResponse Execute(IGraphQLRequest request)
            => _client.Execute(request);

        public T Execute<T>(IGraphQLRequest request)
            => _client.Execute<T>(request);

        public Task<IGraphQLResponse> ExecuteAsync(IGraphQLRequest request, CancellationToken cancellationToken)
            => _client.ExecuteAsync(request, cancellationToken);

        public Task<T> ExecuteAsync<T>(IGraphQLRequest request, CancellationToken cancellationToken)
            => _client.ExecuteAsync<T>(request, cancellationToken);

        #endregion

        #region IPublicContentApi

        public BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, int binaryId,
            IContextData contextData)
        {
            return _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.GetBinaryComponentById,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"binaryId", binaryId},
                    {"contextData", contextData}
                }
            }).BinaryComponent;
        }

        public BinaryComponent GetBinaryComponent(ContentNamespace ns, int publicationId, string url,
            IContextData contextData)
        {
            return _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.GetBinaryComponentByUrl,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"url", url},
                    {"contextData", contextData}
                }
            }).BinaryComponent;
        }

        public KeywordConnection GetKeywords(ContentNamespace ns, int publicationId, IPagination pagination,
            IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.GetKeywords,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"first", pagination.First},
                    {"after", pagination.After},
                    {"contextData", contextData}
                }
            });
            return contenQuery.Categories;
        }

        public Keyword GetKeyword(ContentNamespace ns, int publicationId, int categoryId, int keywordId,
            IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.GetKeyword,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"categoryId", categoryId},
                    {"keywordId", keywordId},
                    {"contextData", contextData}
                }
            });
            return contenQuery.Keyword;
        }

        public ComponentPresentation GetComponentPresentation(ContentNamespace ns, int publicationId, int componentId,
            int templateId, IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.GetComponentPresentation,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"componentId", componentId},
                    {"templateId", templateId},
                    {"contextData", contextData}
                }
            });
            return contenQuery.ComponentPresentation;
        }

        public List<IItem> GetItems(IPagination pagination, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        public Publication GetPublication(ContentNamespace ns, int publicationId, IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.GetPublication,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"contextData", contextData}
                }
            });
            return contenQuery.Publication;
        }

        public PublicationConnection GetPublications(ContentNamespace ns, IPagination pagination,
            InputPublicationFilter publicationFilter, IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.GetPublications,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"first", pagination.First},
                    {"after", pagination.After},
                    {"filterCustomMeta", publicationFilter.Value},
                    {"contextData", contextData}
                }
            });
            return contenQuery.Publications;
        }

        public object GetPublicationMapping(ContentNamespace ns, string uri, IContextData contextData)
        {
            throw new NotImplementedException();
        }

        public PageConnection GetPages(ContentNamespace ns, string url, IPagination pagination, IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.GetPages,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"url", url},
                    {"first", pagination.First},
                    {"after", pagination.After},
                    {"contextData", contextData}
                }
            });
            return contenQuery.Pages;
        }

        public StructureGroupConnection GetStructureGroups(ContentNamespace ns, int publicationId,
            IPagination pagination, IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.GetStructureGroups,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"first", pagination.First},
                    {"after", pagination.After},
                    {"contextData", contextData}
                }
            });
            return contenQuery.StructureGroups;
        }

        public StructureGroup GetStructureGroup(ContentNamespace ns, int publicationId, int structureGroupId,
            IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = Queries.GetStructureGroup,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"structureGroupId", structureGroupId},
                    {"contextData", contextData}
                }
            });
            return contenQuery.StructureGroup;
        }

        #endregion

        #region IModelServicePluginApi

        protected ClaimValue CreateClaim(ContentType contentType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.ModelType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (ContentType), contentType)
        };

        protected ClaimValue CreateClaim(DataModelType dataModelType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.ModelType,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (DataModelType), dataModelType)
        };

        protected ClaimValue CreateClaim(PageInclusion pageInclusion) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.PageIncludeRegions,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof (PageInclusion), pageInclusion)
        };

        protected ClaimValue CreateClaim(DcpType dcpType) => new ClaimValue
        {
            Uri = ModelServiceClaimUris.PageIncludeRegions,
            Type = ClaimValueType.STRING,
            Value = Enum.GetName(typeof(DcpType), dcpType)
        };

        protected void UpdateContextData(ref IContextData contextData, ContentType contentType,
            DataModelType dataModelType, PageInclusion pageInclusion)
        {
            if (contextData == null)
            {
                contextData = new ContextData();
            }

            contextData.ClaimValues.Add(CreateClaim(contentType));
            contextData.ClaimValues.Add(CreateClaim(dataModelType));
            contextData.ClaimValues.Add(CreateClaim(pageInclusion));
        }

        protected void UpdateContextData(ref IContextData contextData, ContentType contentType,
           DataModelType dataModelType, DcpType dcpType)
        {
            if (contextData == null)
            {
                contextData = new ContextData();
            }

            contextData.ClaimValues.Add(CreateClaim(contentType));
            contextData.ClaimValues.Add(CreateClaim(dataModelType));
            contextData.ClaimValues.Add(CreateClaim(dcpType));
        }

        public object GetPageModelData(ContentNamespace ns, int publicationId, int pageId, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData)
        {
            UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = ModelServicePlugin.Queries.GetPageModelDataByPageId,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"pageId", pageId},
                    {"contextData", contextData}
                }
            });
            throw new NotImplementedException();
        }

        public object GetPageModelData(ContentNamespace ns, int publicationId, string url, ContentType contentType,
            DataModelType modelType, PageInclusion pageInclusion, IContextData contextData)
        {
            UpdateContextData(ref contextData, contentType, modelType, pageInclusion);
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = ModelServicePlugin.Queries.GetPageModelDataByUrl,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"url", url},
                    {"contextData", contextData}
                }
            });
            throw new NotImplementedException();
        }

        public object GetEntityModelData(ContentNamespace ns, int publicationId, int entityId, ContentType contentType,
            DataModelType modelType, DcpType dcpType, IContextData contextData)
        {
            UpdateContextData(ref contextData, contentType, modelType, dcpType);

            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = ModelServicePlugin.Queries.GetEntityModelData,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"entityId", entityId},
                    {"contextData", contextData}
                }
            });
            throw new NotImplementedException();
        }

        public object GetSitemap(ContentNamespace ns, int publicationId, IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = ModelServicePlugin.Queries.GetSitemap,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"contextData", contextData}
                }
            });
            throw new NotImplementedException();
        }

        public object GetSitemap(ContentNamespace ns, int publicationId, string taxonomyNodeId, bool includeAncestors,
            IContextData contextData)
        {
            var contenQuery = _client.Execute<ContentQuery>(new GraphQLRequest
            {
                Query = ModelServicePlugin.Queries.GetSitemapSubtree,
                Variables = new Dictionary<string, object>
                {
                    {"namespaceId", ns},
                    {"publicationId", publicationId},
                    {"taxonomyNodeId", taxonomyNodeId},
                    {"includeAncestors", includeAncestors},
                    {"contextData", contextData}
                }
            });
            throw new NotImplementedException();
        }

        #endregion
    }
}
