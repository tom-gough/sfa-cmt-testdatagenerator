using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests
{
    public static class PageExtensions
    {
        public static async Task TypeInputAsync(this Page page, string selector, string text)
        {
            await page.WaitForSelectorAsync(selector);
            await page.FocusAsync(selector);
            await page.Keyboard.TypeAsync(text);
        }

        public static async Task OvertypeInputAsync(this Page page, string selector, string text)
        {
            await page.WaitForSelectorAsync(selector);
            await page.FocusAsync(selector);

            await page.Keyboard.DownAsync("Control");
            await page.Keyboard.PressAsync("A");
            await page.Keyboard.UpAsync("Control");
            await page.Keyboard.PressAsync("Backspace");

            await page.Keyboard.TypeAsync(text);
        }

        public static async Task ClickOn(this Page page, string selector)
        {
            await page.WaitForSelectorAsync(selector);
            await page.ClickAsync(selector);
        }
    }
}
