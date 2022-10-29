[![Build status](https://punke.visualstudio.com/Blazor/_apis/build/status/Logixware.AspNet.Blazor.Fluxor%20CI)](https://punke.visualstudio.com/DotNetStandard/_build/latest?definitionId=40)

# Introduction 
Extensions for [Blazor Fluxor](https://github.com/mrpmorris/blazor-fluxor).

## 1. Reactive extensions

### Usage

#### On app init: 

```csharp
services.AddFluxor(options => options
   ....
   .AddReactiveStore(services)
);
```

#### State inside a component: 
```csharp
@inject IReactiveStore Store

protected override void OnInit()
{
    this.Store.States

        .TakeUntil(this._IsDisposed)
        .TakeState<SidebarsState>()
        .Where(state => state.Sidebars.ContainsKey(this.SidebarPosition))
        .Subscribe(state =>
        {
            if (this._IsMobile)
            {
                this._IsFullScreen = state.Sidebars[this.SidebarPosition].IsOpen;
                base.StateHasChanged();
            }
        });
}
```

#### Actions inside a component: 
```csharp
@inject IReactiveStore Store

protected override void OnInit()
{
    this.Store.Actions

        .TakeAction<OpenMobileSidebarAction>()
        .Where(action => action.SidebarPosition == this.SidebarPosition)
        .Subscribe(action =>
        {
            this._IsFullScreen = this._IsMobile;
            base.StateHasChanged();
        });

    this.Store.Actions

        .TakeAction<CloseMobileSidebarAction>()
        .Where(action => action.SidebarPosition == this.SidebarPosition)
        .Subscribe(action =>
        {
            this._IsFullScreen = false;
            base.StateHasChanged();
        });
}
```

# Contribute
Feel free to open issues and issue pull requests.
