import React from 'react';
 const HomeBanner = () => {
    return (
        <div>
            <div className="bg-[url(/pexels-tirachard-kumtanom-112571-733856.jpg)] sm:bg-cover bg-no-repeat h-screen max-h-[70vh]" data-testid="image">
                <div className="pt-50 ps-10">
                    <h1 className="mb-4 text-3xl font-extrabold text-gray-900 dark:text-white md:text-5xl lg:text-6xl"><span className="text-transparent bg-clip-text bg-gradient-to-r from-white to-green-950">Internship Learning</span></h1>
                    <p className="text-md text-gray-500 lg:text-xl dark:text-green-950 font-extrabold sm:w-xl text-justify pe-25">Here at Company we focus on markets where technology, innovation, and capital can unlock long-term value and drive economic growth.</p>
                </div>
            </div>
        </div>
    )   
}
export default HomeBanner