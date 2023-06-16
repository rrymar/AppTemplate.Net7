import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UsersModule } from './users/users.module';
import { NgxsModule } from '@ngxs/store';
import { environment } from '../environments/environment';
import { HttpClientModule } from '@angular/common/http';
import { RouteReuseStrategy } from '@angular/router';
import { CustomRouteReuseStrategy } from './core/custom-route-reuse-strategy';
import { NgxsRouterPluginModule } from '@ngxs/router-plugin';
import { CoreModule } from './core/core.module';
import { GlobalErrorsHandler } from './core/errors/global-errors-handler';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    NgxsModule.forRoot([], { developmentMode: !environment.production }),
    NgxsReduxDevtoolsPluginModule.forRoot(),
    NgxsRouterPluginModule.forRoot(),
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    UsersModule,
    CoreModule
  ],
  providers: [
    {
      provide: RouteReuseStrategy,
      useClass: CustomRouteReuseStrategy
    },
    {
      provide: ErrorHandler,
      useClass: GlobalErrorsHandler
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
