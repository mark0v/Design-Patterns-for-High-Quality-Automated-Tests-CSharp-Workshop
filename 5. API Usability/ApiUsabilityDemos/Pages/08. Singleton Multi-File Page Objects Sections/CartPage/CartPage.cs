﻿// Copyright 2022 Automate The Planet Ltd.
// Author: Anton Angelov
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
namespace ApiUsabilityDemos.Eight
{
    public class CartPage : NavigatableEShopPage<CartPage>
    {
        private readonly IBrowserService _browserService;

        public CartPage() 
        {
            _browserService = LoggingSingletonDriver.Instance;
            BreadcrumbSection = new BreadcrumbSection(LoggingSingletonDriver.Instance);
            Elements = new CartPageElements(LoggingSingletonDriver.Instance);
        }

        public BreadcrumbSection BreadcrumbSection { get; }
        public CartPageElements Elements { get; }

        protected override string Url => "http://demos.bellatrix.solutions/cart/";


        public void ApplyCoupon(string coupon)
        {
            Elements.CouponCodeTextField.TypeText(coupon);
            Elements.ApplyCouponButton.Click();
            _browserService.WaitForAjax();
        }

        public void IncreaseProductQuantity(int newQuantity)
        {
            Elements.QuantityBox.TypeText(newQuantity.ToString());
            Elements.UpdateCart.Click();
            _browserService.WaitForAjax();
        }

        public void ProceedToCheckout()
        {
            Elements.ProceedToCheckout.Click();
            _browserService.WaitUntilPageLoadsCompletely();
        }

        public string GetTotal()
        {
            return Elements.TotalSpan.Text;
        }


        public string GetMessageNotification()
        {
            return Elements.MessageAlert.Text;
        }

        protected override void WaitForPageLoad()
        {
            Elements.CouponCodeTextField.WaitToExists();
        }
    }
}
