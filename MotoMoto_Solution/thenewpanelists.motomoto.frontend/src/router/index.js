import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import DirectMessage from '../views/DirectMessageView.vue'
import Login from '../views/LoginView.vue'
import CommunityDashboard from '../views/CommunityView.vue'
import PartFlaggingBuilder from '../views/PartFlaggingBuilderView.vue'
import PartFlaggingPost from '../views/PartFlaggingPostView.vue'
import EventList from '../views/EventListView.vue'
import Registration from '../views/RegistrationView.vue'
import PartPriceAnalysis from '../views/PartPriceAnalysisView.vue'
import PartPriceDetails from '../views/PartPriceDetailsView.vue'
import PartComparison from '../views/PartPriceAnalysisComparisonView.vue'
import UserProfile from '../views/UserProfileView.vue'
import EditPreferences from '../views/EditPreferencesView'


const routes = [
  {
    path:'/',
    name: 'HomeView',
    component: HomeView
  },
  {
    path:'/Login',
    name: 'Login',
    component: Login
  },
  {
    path: '/DM',
    name: 'DirectMessage',
    component: DirectMessage
  },
  {
    path: '/CommunityDashboard',
    name: 'CommunityDashboard',
    component: CommunityDashboard
  },
  {
    path: '/PartFlaggingBuilder',
    name: 'PartFlaggingCarBuilder',
    component: PartFlaggingBuilder
  },
  {
    path: '/PartFlaggingPost',
    name: 'PartFlaggingPost',
    component: PartFlaggingPost
  },
  {
    path: '/EventList',
    name: 'EventList',
    component: EventList
  },
  {
    path: '/Registration',
    name: 'Registration',
    component: Registration
  },
  {
    path: '/Parts',
    name: 'PartPriceAnalysis',
    component: PartPriceAnalysis
  },
  {
    path: '/PartDetails/:id',
    name: 'PartPriceDetails',
    component: PartPriceDetails
  },
  {
    path: '/PartComaprison/PartOne/:id1/PartTwo/:id2',
    name: 'PartComparison',
    component: PartComparison
  },
  {
    path: '/UserProfile',
    name: 'UserProfile',
    component: UserProfile
  },
  {
    path: '/EditPreferences',
    name: 'EditPreferences',
    component: EditPreferences
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
