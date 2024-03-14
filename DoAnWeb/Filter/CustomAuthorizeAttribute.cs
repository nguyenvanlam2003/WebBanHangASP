using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DoAnWeb.Filter
{
    public class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            if (httpContext.Session["AdId"] == null)
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang đăng nhập
                httpContext.Response.Redirect("~/Admin/Account/Login");
                return false; // Trả về false để chỉ ra rằng người dùng không được phép truy cập
            }
            return true; // Trả về true nếu người dùng đã đăng nhập
        }
    }
}