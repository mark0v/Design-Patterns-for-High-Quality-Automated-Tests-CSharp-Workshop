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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestsMaintainabilityDemos.Facades.First
{
    [TestClass]
    public class FacadeTests
    {
        private static Driver _driver;
        private static MainPage _mainPage;
        private static CartPage _cartPage;
        private static CheckoutPage _checkoutPage;
        private static PurchaseFacade _purchaseFacade;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _driver = new LoggingDriver(new WebDriver());
            _driver.Start(Browser.Chrome);
            _mainPage = new MainPage(_driver);
            _cartPage = new CartPage(_driver);
            _checkoutPage = new CheckoutPage(_driver);
            _purchaseFacade = new PurchaseFacade(_mainPage, _cartPage, _checkoutPage);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void PurchaseFalcon9WithoutFacade()
        {
            _mainPage.Open();
            _mainPage.AddRocketToShoppingCart("Falcon 9");
            _cartPage.ApplyCoupon("happybirthday");
            _cartPage.AssertCouponAppliedSuccessfully();
            _cartPage.IncreaseProductQuantity(2);
            _cartPage.AssertTotalPrice("114.00€");
            _cartPage.ClickProceedToCheckout();

            var purchaseInfo = new PurchaseInfo()
                               {
                                   Email = "info@berlinspaceflowers.com",
                                   FirstName = "Anton",
                                   LastName = "Angelov",
                                   Company = "Space Flowers",
                                   Country = "Germany",
                                   Address1 = "1 Willi Brandt Avenue Tiergarten",
                                   Address2 = "Lützowplatz 17",
                                   City = "Berlin",
                                   Zip = "10115",
                                   Phone = "+00498888999281",
                               };
            _checkoutPage.FillBillingInfo(purchaseInfo);
            _checkoutPage.AssertOrderReceived();
        }

        [TestMethod]
        public void PurchaseSaturnVWithoutFacade()
        {
            _mainPage.Open();
            _mainPage.AddRocketToShoppingCart("Saturn V");
            _cartPage.ApplyCoupon("happybirthday");
            _cartPage.AssertCouponAppliedSuccessfully();
            _cartPage.IncreaseProductQuantity(3);
            _cartPage.AssertTotalPrice("355.00€");
            _cartPage.ClickProceedToCheckout();

            var purchaseInfo = new PurchaseInfo()
                               {
                                   Email = "info@berlinspaceflowers.com",
                                   FirstName = "John",
                                   LastName = "Atanasov",
                                   Company = "Space Flowers",
                                   Country = "Germany",
                                   Address1 = "1 Willi Brandt Avenue Tiergarten",
                                   Address2 = "Lützowplatz 17",
                                   City = "Berlin",
                                   Zip = "10115",
                                   Phone = "+00498888999281",
                               };
            _checkoutPage.FillBillingInfo(purchaseInfo);
            _checkoutPage.AssertOrderReceived();
        }
    }
}
