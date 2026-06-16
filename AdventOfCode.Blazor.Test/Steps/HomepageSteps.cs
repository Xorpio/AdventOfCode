using AdventOfCode.Blazor.Test.Drivers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AdventOfCode.Blazor.Test.Steps;

[Binding]
public class HomepageSteps
{
    private readonly Driver _driver;

    public HomepageSteps(Driver driver)
    {
        _driver = driver;
    }

    [Given(@"the user is on the website")]
    public async Task GivenTheUserIsOnTheWebsiteAsync()
    {
        await _driver.Page.GotoAsync("/login");
        //await Page.GotoAsync(RootUri.AbsoluteUri);
    }

    [When(@"the user navigates to the home page")]
    public async Task WhenTheUserNavigatesToTheHomePageAsync()
    {
        //await Page.GotoAsync(RootUri.AbsoluteUri);
    }

    [Then(@"the home page should be displayed")]
    public async Task ThenTheHomePageShouldBeDisplayedAsync()
    {
        //await Page.PauseAsync();
        //await Page.GotoAsync(RootUri.AbsoluteUri);
    }


    [Then(@"the home page should contain the main content sections")]
    public void ThenTheHomePageShouldContainTheMainContentSections()
    {
        throw new PendingStepException();
    }

    [Then(@"the home page should have navigation options")]
    public void ThenTheHomePageShouldHaveNavigationOptions()
    {
        throw new PendingStepException();
    }


}
