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
namespace ApiUsabilityDemos.Ninth
{
    public class MainPage : NavigatableEShopPage
    {
        private MainPage(Driver driver) 
            : base(driver)
        {
            Elements = new MainPageElements(driver);
            Assertions = new MainPageAssertions(Elements);
        }

        public MainPageElements Elements { get; set; }
        public MainPageAssertions Assertions { get; set; }

        protected override string Url => "http://demos.bellatrix.solutions/";

        public void AddRocketToShoppingCart()
        {
            Open();
            Elements.AddToCartFalcon9.Click();
            Elements.ViewCartButton.Click();
        }

        protected override void WaitForPageLoad()
        {
            Elements.AddToCartFalcon9.WaitToExists();
        }
    }
}
