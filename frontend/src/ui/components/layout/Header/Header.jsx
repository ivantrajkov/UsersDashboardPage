import { useState } from "react"
import {useNavigate} from "react-router-dom"
import { NavLink } from "react-router-dom"
import useAuth from "../../../../hooks/useAuth.js"
 const Header = () => {
  const [showMenu, setShowMenu] = useState(false)
    const navigate = useNavigate();
  const {user} = useAuth()
  const {logout} = useAuth()
  const handleLogout = () =>{
    logout()
    navigate("/loggedOut")    
  }
    return (
        // <div className="bg-black text-white font-sans flex sm:h-10 items-center justify-between">
        //     <a className="hover:cursor-pointer hover:underline">Frontend Authentication app</a>
        //     <a href="#" className="hover:cursor-pointer hover:underline">option</a>
        //     <a href="#" className="hover:cursor-pointer hover:underline">option</a>
        //     <a href="#" className="hover:cursor-pointer hover:underline">option</a>
        // </div>
        <nav className="bg-gray-800">
  <div className="mx-auto max-w-7xl px-2 sm:px-6 lg:px-8">
    <div className="relative flex h-16 items-center justify-between">
      <div className="absolute inset-y-0 left-0 flex items-center sm:hidden">
        {/* <!-- Mobile menu button--> */}
        <button type="button" className="relative inline-flex items-center justify-center rounded-md p-2 text-gray-400 hover:bg-gray-700 hover:text-white focus:ring-2 focus:ring-white focus:outline-hidden focus:ring-inset" aria-controls="mobile-menu" aria-expanded="false">
          <span className="absolute -inset-0.5"></span>
          <span className="sr-only">Open main menu</span>
          {/* <!--
            Icon when menu is closed.

            Menu open: "hidden", Menu closed: "block"
          --> */}
          <svg className="block size-6" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" aria-hidden="true" data-slot="icon">
            <path strokeLinecap="round" strokeLinejoin="round" d="M3.75 6.75h16.5M3.75 12h16.5m-16.5 5.25h16.5" />
          </svg>
          {/* <!--
            Icon when menu is open.

            Menu open: "block", Menu closed: "hidden"
          --> */}
          <svg className="hidden size-6" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" aria-hidden="true" data-slot="icon">
            <path strokeLinecap="round" strokeLinejoin="round" d="M6 18 18 6M6 6l12 12" />
          </svg>
        </button>
      </div>
      <div className="flex flex-1 items-center justify-center sm:items-stretch sm:justify-start">
        <div className="flex shrink-0 items-center">
          <img className="h-8 w-auto" src="https://tailwindcss.com/plus-assets/img/logos/mark.svg?color=indigo&shade=500" alt="Your Company"/>
        </div>
         <div className="hidden sm:ml-6 sm:block">
              <div className="flex space-x-4">
                <NavLink
                  to="/"
                  className={({ isActive }) =>
                    `rounded-md px-3 py-2 text-sm font-medium ${
                      isActive ? 'bg-gray-900 text-white' : 'text-gray-300 hover:bg-gray-700 hover:text-white'
                    }`
                  }
                  aria-current="page"
                >
                  Home
                </NavLink>
                <NavLink
                  to="/dashboard"
                  className={({ isActive }) =>
                    `rounded-md px-3 py-2 text-sm font-medium ${
                      isActive ? 'bg-gray-900 text-white' : 'text-gray-300 hover:bg-gray-700 hover:text-white'
                    }`
                  }
                >
                  Dashboard
                </NavLink>
                {/* <NavLink
                  to="/login" // Add actual paths
                  className={({ isActive }) =>
                    `rounded-md px-3 py-2 text-sm font-medium ${
                      isActive ? 'bg-gray-900 text-white' : 'text-gray-300 hover:bg-gray-700 hover:text-white'
                    }`
                  }
                >
                  Login
                </NavLink> */}
                {/* <NavLink
                  to="/option2" // Add actual paths
                  className={({ isActive }) =>
                    `rounded-md px-3 py-2 text-sm font-medium ${
                      isActive ? 'bg-gray-900 text-white' : 'text-gray-300 hover:bg-gray-700 hover:text-white'
                    }`
                  }
                >
                  Option
                </NavLink> */}
              </div>
            </div>
      </div>
      <div className="absolute inset-y-0 right-0 flex items-center pr-2 sm:static sm:inset-auto sm:ml-6 sm:pr-0">
        <button type="button" className="relative rounded-full bg-gray-800 p-1 text-gray-400 hover:text-white focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800 focus:outline-hidden">
          <span className="absolute -inset-1.5"></span>
          <span className="sr-only">View notifications</span>
          <svg className="size-6" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" aria-hidden="true" data-slot="icon">
            <path strokeLinecap="round" strokeLinejoin="round" d="M14.857 17.082a23.848 23.848 0 0 0 5.454-1.31A8.967 8.967 0 0 1 18 9.75V9A6 6 0 0 0 6 9v.75a8.967 8.967 0 0 1-2.312 6.022c1.733.64 3.56 1.085 5.455 1.31m5.714 0a24.255 24.255 0 0 1-5.714 0m5.714 0a3 3 0 1 1-5.714 0" />
          </svg>
        </button>

        {/* <!-- Profile dropdown --> */}
        
        <div className="relative ml-3">
          <div>
            {user &&
             <button type="button" className="relative flex rounded-full bg-gray-800 text-sm focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800 focus:outline-hidden" id="user-menu-button" aria-expanded="false" aria-haspopup="true" onClick={() => setShowMenu(!showMenu)} >
              <span className="absolute -inset-1.5"></span>
              <span className="sr-only">Open user menu</span>
              <img className="size-8" src="/avatar-svgrepo-com.svg" alt=""/>
            </button>
            }
            {!user && 
            <button type="button" className=" text-2xl pb-1 text-white relative flex rounded-full bg-gray-800  focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800 focus:outline-hidden" id="user-menu-button" aria-expanded="false" aria-haspopup="true" onClick={() => setShowMenu(!showMenu)} >
              <span className="absolute -inset-1.5"></span>
              <span className="sr-only">Open user menu</span>
              {/* <img className="size-8" src="/avatar-svgrepo-com.svg" alt=""/> */}
              |||
            </button>
            }
           
          </div>
          {showMenu && user &&
          <div className="absolute right-0 z-10 mt-2 w-48 origin-top-right rounded-md bg-white py-1 shadow-lg ring-1 ring-black/5 focus:outline-hidden" role="menu" aria-orientation="vertical" aria-labelledby="user-menu-button" tabIndex="-1"> 
            <a href="#" className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-300" role="menuitem" tabIndex="-1" id="user-menu-item-2" onClick={handleLogout}>Sign out</a> 
           </div>
                   }
                   {showMenu && !user &&
          <div className="absolute right-0 z-10 mt-2 w-48 origin-top-right rounded-md bg-white py-1 shadow-lg ring-1 ring-black/5 focus:outline-hidden" role="menu" aria-orientation="vertical" aria-labelledby="user-menu-button" tabIndex="-1"> 
            <a href="/login" className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-300" role="menuitem" tabIndex="-1" id="user-menu-item-0" >Login</a>
            <a href="/register" className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-300" role="menuitem" tabIndex="-1" id="user-menu-item-1">Register</a>
           </div>
                   }
        </div>
      </div>
    </div>
  </div>

  {/* <!-- Mobile menu, show/hide based on menu state. --> */}
  <div className="sm:hidden" id="mobile-menu">
    <div className="space-y-1 px-2 pt-2 pb-3">
      {/* <!-- Current: "bg-gray-900 text-white", Default: "text-gray-300 hover:bg-gray-700 hover:text-white" --> */}
      <a href="#" className="block rounded-md bg-gray-900 px-3 py-2 text-base font-medium text-white" aria-current="page">Dashboard</a>
      <a href="#" className="block rounded-md px-3 py-2 text-base font-medium text-gray-300 hover:bg-gray-700 hover:text-white">Team</a>
      <a href="#" className="block rounded-md px-3 py-2 text-base font-medium text-gray-300 hover:bg-gray-700 hover:text-white">Projects</a>
      <a href="#" className="block rounded-md px-3 py-2 text-base font-medium text-gray-300 hover:bg-gray-700 hover:text-white">Calendar</a>
    </div>
  </div>
</nav>
    )
    
}
export default Header;