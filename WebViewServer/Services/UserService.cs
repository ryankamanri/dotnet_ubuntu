using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Kamanri.Http;
using WebViewServer.Models;
using WebViewServer.Models.Post;
using WebViewServer.Models.User;

namespace WebViewServer.Services
{
	public class UserService
	{

		private Api _api;



		#region Init


		public UserService(Api api)
		{
			_api = api;

		}



		#endregion

		#region Get

		public async Task<User> GetUser(string account)
		{
			return await _api.Post<User>("/User/GetUser", new Form()
			{
				{"Account", account}
			});
		}
		public async Task<UserInfo> GetUserInfo(User user)
		{
			return await _api.Post<UserInfo>("/User/GetUserInfo", new Form()
			{
				{"User", user}
			});
		}

		#endregion

		#region Select


		/// <summary>
		/// 用户选择标签
		/// </summary>
		/// <param name="type"></param>
		/// <param name="SetSelections"></param>
		/// <returns></returns>
		public async Task<IList<Tag>> SelectTag(User user, Action<dynamic> SetSelections)
		{
			dynamic selections = new ExpandoObject();
			SetSelections(selections);
			return await _api.Post<IList<Tag>>("/User/SelectTag", new Form()
			{
				{"User", user},
				{"Selections", selections}
			});
		}

		public async Task<IList<Post>> SelectPost(User user, Action<dynamic> SetSelections)
		{
			dynamic selections = new ExpandoObject();
			SetSelections(selections);
			return await _api.Post<IList<Post>>("/User/SelectPost", new Form()
			{
				{"User", user},
				{"Selections", selections}
			});
		}

		public async Task<IList<Comment>> SelectComment(User user, Action<dynamic> SetSelections)
		{
			dynamic selections = new ExpandoObject();
			SetSelections(selections);
			return await _api.Post<IList<Comment>>("/User/SelectComment", new Form()
			{
				{"User", user},
				{"Selections", selections}
			});
		}

		/// <summary>
		/// 选择你主动去采取行为的用户.例如关注,拉黑
		/// </summary>
		/// <param name="user"></param>
		/// <param name="SetSelections"></param>
		/// <returns></returns>
		public async Task<IList<User>> SelectUserInitiative(User user, Action<dynamic> SetSelections)
		{
			dynamic selections = new ExpandoObject();
			SetSelections(selections);
			return await _api.Post<IList<User>>("/User/SelectUserInitiative", new Form()
			{
				{"User", user},
				{"Selections", selections}
			});
		}

		/// <summary>
		/// 选择对你采取行为的用户.例如关注,拉黑
		/// </summary>
		/// <param name="user"></param>
		/// <param name="SetSelections"></param>
		/// <returns></returns>
		public async Task<IList<User>> SelectUserPassive(User user, Action<dynamic> SetSelections)
		{
			dynamic selections = new ExpandoObject();
			SetSelections(selections);
			return await _api.Post<IList<User>>("/User/SelectUserPassive", new Form()
			{
				{"User", user},
				{"Selections", selections}
			});
		}

		#endregion


		#region Mapping


		public async Task<ICollection<Post>> MappingPostsByTag(User user, Action<dynamic> SetUserTagRelation)
		{
			dynamic selections = new ExpandoObject();
			SetUserTagRelation(selections);
			return await _api.Post<ICollection<Post>>("/User/MappingPostsByTag", new Form()
			{
				{"User", user},
				{"Selections", selections}
			});
		}



		#endregion

		#region Change

		public async Task<long> InsertUser(User user)
		{
			return await _api.Post<long>("/User/InsertUser", new Form()
			{
				{"User",user}
			});
		}

		public async Task<string> InsertOrUpdateUserInfo(UserInfo userInfo)
		{
			return await _api.Post<string>("/User/InsertOrUpdateUserInfo", new Form()
			{
				{"UserInfo", userInfo}
			});
		}
		#endregion

		#region JudgeRelation

		/// <summary>
		/// 判断某两个用户的特定关系是否存在
		/// </summary>
		/// <param name="keyUser"></param>
		/// <param name="valueUser"></param>
		/// <param name="relationName"></param>
		/// <param name="relationValue"></param>
		/// <returns></returns>
		public async Task<bool> IsUserRelationExist(User keyUser, User valueUser, string relationName, string relationValue)
		{
			return await _api.Post<bool>("/User/IsUserRelationExist", new Form()
			{
				{"KeyUser", keyUser},
				{"ValueUser", valueUser},
				{"RelationName", relationName},
				{"RelationValue", relationValue}
			});
		}

		#endregion


		#region ChangeRelation

		#region Framework


		/// <summary>
		/// 新增本用户与实体之间的关系
		/// </summary>
		/// <param name="tag"></param>
		/// <param name="tagRelation"></param>
		/// <returns></returns>
		public async Task<bool> AppendRelation(User user, string entityType, long ID, string relationName, string newRelation)
		{
			return await _api.Post<bool>("/User/AppendRelation", new Form()
			{
				{"User",user},
				{"EntityType",entityType},
				{"ID",ID},
				{"RelationName",relationName},
				{"Relation",newRelation}
			});
		}



		/// <summary>
		/// 移除本用户与实体之间的关系
		/// </summary>
		/// <param name="tag"></param>
		/// <param name="tagRelation"></param>
		/// <returns></returns>
		public async Task<bool> RemoveRelation(User user, string entityType, long ID, string relationName, string oldRelation)
		{
			return await _api.Post<bool>("/User/RemoveRelation", new Form()
			{
				{"User",user},
				{"EntityType",entityType},
				{"ID",ID},
				{"RelationName",relationName},
				{"Relation",oldRelation}
			});
		}

		#endregion



		#endregion


		#region Carculate

		


		public async Task<double> CarculateSimilarityFix(User user_1, User user_2, Action<dynamic> SetMyTagsType)
		{

			dynamic selections = new ExpandoObject();
			SetMyTagsType(selections);
			return await _api.Post<double>("/User/CarculateSimilarityFix", new Form()
			{
				{"User_1", user_1},
				{"User_2", user_2},
				{"Selections", selections}
			});
		}
		#endregion
	}
}