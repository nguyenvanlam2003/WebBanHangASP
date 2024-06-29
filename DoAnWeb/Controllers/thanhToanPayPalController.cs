﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using PayPal.Api;

namespace DoAnWeb.Controllers
{
    public class thanhToanPayPalController : Controller
    {
        // GET: thanhToanPayPal
        private MyDBconText db = new MyDBconText();
   
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext =PaypalConfiguration.GetAPIContext();

            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/thanhToanPayPal/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return RedirectToAction("FailureView", "GioHang");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("FailureView","GioHang");
            }
            HoaDon hoad = TempData["HoaDon"] as HoaDon;
            //on successful payment, show success page to user.  
            var ketqua = (from gh in db.GioHangs
                          join ctGH in db.ChiTietGioHangs on gh.maGH equals ctGH.maGH
                          join sp in db.SanPhams on ctGH.maSP equals sp.maSP
                          where gh.maTK == hoad.maTK
                          select new
                          {
                              gh.maTK,
                              ctGH.maSP,
                              ctGH.ID,
                              ctGH.gia,
                              ChiTietGioHangSoLuong = ctGH.soLuong,
                              ctGH.thanhTien,
                              // Thêm các trường khác cần thiết từ các bảng khác
                          }).ToList();
            TaoMaTuDong mHD = new TaoMaTuDong();
            if (hoad.hinhThucThanhToan.ToString() == "banking")
            {
                db.HoaDons.Add(hoad);
                db.SaveChanges();
                int i = 0;
                foreach (var item in ketqua)
                {
                    i++;
                    ChiTietHD ctHD = new ChiTietHD
                    {
                        ID = mHD.sinhMa("CTHD_") + i,
                        maHD = hoad.maHD,
                        maSP = item.maSP,
                        gia = item.gia,
                        soLuong = item.ChiTietGioHangSoLuong,
                        thanhTien = item.thanhTien
                    };
                    db.ChiTietHDs.Add(ctHD);
                    ChiTietGioHang cthGH = new ChiTietGioHang();
                    cthGH = db.ChiTietGioHangs.Where(row => row.ID == item.ID).FirstOrDefault();
                    db.ChiTietGioHangs.Remove(cthGH);
                }
                db.SaveChanges();
            }
            return RedirectToAction("SuccessView","GioHang");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            string userId = Request.Cookies["UserSession"]["UserId"];
            var ketqua = (from gh in db.GioHangs
                          join ctGH in db.ChiTietGioHangs on gh.maGH equals ctGH.maGH
                          join sp in db.SanPhams on ctGH.maSP equals sp.maSP
                          where gh.maTK == userId
                          select new
                          {
                              ctGH.maSP,
                              sp.tenSP,
                              ctGH.gia,
                              ChiTietGioHangSoLuong = ctGH.soLuong,
                              // Thêm các trường khác cần thiết từ các bảng khác
                          }).ToList();
            var tongTien = (from gh in db.GioHangs
                            where gh.maTK == userId
                            select new
                            {
                                gh.tongTien,
                                gh.soLuong
                            }).FirstOrDefault();
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc
            foreach (var item in ketqua)
            {
                itemList.items.Add(new Item()
                {
                    name = item.tenSP.ToString(),
                    currency = "USD",
                    price = Math.Round(item.gia.Value / 24950, 2).ToString(),
                    quantity = item.ChiTietGioHangSoLuong.ToString()
                });
            }
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = Math.Round(tongTien.tongTien.Value / 24950, 2).ToString()
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = Math.Round(tongTien.tongTien.Value / 24950, 2).ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(), //Generate an Invoice No    
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
    }
}